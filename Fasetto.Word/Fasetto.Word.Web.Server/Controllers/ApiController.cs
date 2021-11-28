﻿using Fasetto.Word.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fasetto.Word.Web.Server
{
    /// <summary>
    /// Manages the web API calles
    /// </summary>
    [AuthorizeToken]
    public class ApiController : Controller
    {
        #region Protected Members

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="signInManager">The Identity sign in manager</param>
        /// <param name="userManager">The Identity user manager</param>
        public ApiController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion

        #region Login / Register  / Verify

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="RegisterCredentials">The registeration details</param>
        /// <returns>Returns the result of the register request</returns>
        [AllowAnonymous]
        [Route(ApiRoutes.Register)]
        public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync([FromBody] RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Please provide all required details to register for an account";

            // The error response for a failed login
            var errorResponse = new ApiResponse<RegisterResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // If we have no credentials...
            if (registerCredentials == null)
                // Return failed response
                return errorResponse;

            // Make sure we have a user name
            if (string.IsNullOrWhiteSpace(registerCredentials.Username))
                // Return error message to user
                return errorResponse;

            // Create a desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email,
            };

            // Get the user details
            var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

            // Try and create a user
            var result = await mUserManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was successfull...
            if (result.Succeeded)
            {
                // Send email verification
                await SendUserEmailVerificationAsync(user);

                // Return valid response containing all users details
                return new ApiResponse<RegisterResultApiModel>
                {
                    Response = new RegisterResultApiModel
                    {
                        FirstName = userIdentity.FirstName,
                        LastName = userIdentity.LastName,
                        Email = userIdentity.Email,
                        Username = userIdentity.UserName,
                        Token = userIdentity.GenerateJwtToken()
                    }
                };
            }
            // Otherwise if it failed...
            else
                // Return the failed response
                return new ApiResponse<RegisterResultApiModel>
                {
                    // Aggregate all errors into a single error string
                    ErrorMessage = result.Errors?.AggregateErrors()
                };
        }

        /// <summary>
        /// logs in a user using token-based authentication
        /// </summary>
        /// <param name="loginCredentials">The login details</param>
        /// <returns>Returns the result of the Login request</returns>
        [AllowAnonymous]
        [Route(ApiRoutes.Login)]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> LogInAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new ApiResponse<UserProfileDetailsApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Make sure we have a user name
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
                // Return error message to user
                return errorResponse;

            // Validate if the user credentials are correct

            // Is it an email?
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get user details
            var user = isEmail ? 
                // Find by email
                await mUserManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by Username
                await mUserManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
                // Return error message to user
                return errorResponse;

            // If we got here we have a user...
            // Let's validate the password

            // Get if password is valid
            var isValidPassword = await mUserManager.CheckPasswordAsync(user, loginCredentials.Password);

            if (!isValidPassword)
                // Return error message to user
                return errorResponse;

            // If we get here, we are valid and the user passed the correct login details

            // Get username
            var username = user.UserName;

            // Return token to user
            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                    Token = user.GenerateJwtToken(),
                }
            };
        }

        [AllowAnonymous]
        [Route(ApiRoutes.VerigyEmail)]
        [HttpGet]
        public async Task<ActionResult> VerifyEmailAsync(string userId, string emailToken)
        {
            // Get the user
            var user = await mUserManager.FindByIdAsync(userId);

            // If the user is null
            if (user == null)
                // TODO: Nice UI
                return Content("User not found");

            // If we have a user...

            // Verify the email token
            var result = await mUserManager.ConfirmEmailAsync(user, emailToken);

            // If succeeded...
            if (result.Succeeded)
                // TODO: Nice UI
                return Content("Email Verified");

            // TODO: Nice UI
            return Content("Invalid Email Verification Token");
        }

        #endregion

        /// <summary>
        /// Returns the users profile details based on authenticated user
        /// </summary>
        /// <returns></returns>
        [Route(ApiRoutes.GetUserProfile)]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> GetUserProfileAsync()
        {
            // Get user claims
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user
            if (user == null)
                // Return error
                return new ApiResponse<UserProfileDetailsApiModel>()
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            // Return token to user
            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                }
            };
        }

        /// <summary>
        /// Attempts to update the users profile details
        /// </summary>
        /// <param name="model">The user profile details to update</param>
        /// <returns>
        /// Returns successful response if the update was succeed,
        /// otherwise returns the error reasons for the failure
        /// </returns>
        [Route(ApiRoutes.UpdateUserProfile)]
        public async Task<ApiResponse> UpdateUserProfileAsync([FromBody]UpdateUserProfileApiModel model)
        {
            #region Declare Variables

            // Make a list of empty errors
            var errors = new List<string>();

            // Keep track of email change
            var emailChangee = false;

            #endregion

            #region Get User

            // Get the current user
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                return new ApiResponse
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            #endregion

            #region Update Profile

            // If we have a first name
            if (model.FirstName != null)
                // Update the profile details
                user.FirstName = model.FirstName;

            // If we have a last name
            if (model.LastName != null)
                // Update the profile details
                user.LastName = model.LastName;

            // If we have a username
            if (model.Username != null)
                // Update the profile details
                user.UserName = model.Username;

            // If we have a Email
            if (model.Email != null && 
                // And it is not the same 
                !string.Equals(model.Email.Replace(" ", ""), user.NormalizedEmail))
            {
                // Update the Email
                user.Email = model.Email;

                // Un-verify the email
                user.EmailConfirmed = false;

                // Flag we have change email
                emailChangee = true;
            }

            #endregion

            #region Save Profile

            // Attempt to commit changes to data store
            var result = await mUserManager.UpdateAsync(user);
            
            // If successful, send out email verification
            if (result.Succeeded && emailChangee)
                // Send email verification
                await SendUserEmailVerificationAsync(user);

            #endregion

            #region Respond

            // If we were successful
            if (result.Succeeded)
                // Return successful response
                return new ApiResponse();
            // Otherwise
            else
                // Return the failed response
                return new ApiResponse()
                {
                    ErrorMessage = result.Errors?.AggregateErrors()
                };

            #endregion
        }

        /// <summary>
        /// Attempts to update the users password
        /// </summary>
        /// <param name="model">The user password details to update</param>
        /// <returns>
        /// Returns successful response if the update was succeed,
        /// otherwise returns the error reasons for the failure
        /// </returns>
        [Route(ApiRoutes.UpdateUserPassword)]
        public async Task<ApiResponse> UpdateUserPasswordAsync([FromBody] UpdateUserPasswordApiModel model)
        {
            #region Declare Variables

            // Make a list of empty errors
            var errors = new List<string>();

            #endregion

            #region Get User

            // Get the current user
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                return new ApiResponse
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            #endregion

            #region Update Password

            // Attempt to commit changes to data store
            var result = await mUserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            #endregion

            #region Respond

            // If we were successful
            if (result.Succeeded)
                // Return successful response
                return new ApiResponse();
            // Otherwise
            else
                // Return the failed response
                return new ApiResponse()
                {
                    ErrorMessage = result.Errors?.AggregateErrors()
                };

            #endregion
        }

        #region Private Healpers

        /// <summary>
        /// Sends the given user a new verify email link
        /// </summary>
        /// <param name="user">The user to send the link to</param>
        /// <returns></returns>
        private async Task SendUserEmailVerificationAsync(ApplicationUser user)
        {
            // Get the user details
            var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

            // Generate an email verification code
            var emailVerificationCode = await mUserManager.GenerateEmailConfirmationTokenAsync(user);

            // TODO: Replace with APIRoutes that contain the static route to use
            var confirmaionUrl = $"https://{Request.Host.Value}/api/verify/email/?userId={HttpUtility.UrlEncode(userIdentity.Id)}&emailToken={HttpUtility.UrlEncode(emailVerificationCode)}";

            // Email the user the verification code
            await FasettoEmailSender.SendUserVerificationEmailAsync(user.UserName, userIdentity.Email, confirmaionUrl);

        }
        
        #endregion
    }
}
