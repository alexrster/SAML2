﻿using System;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using SAML2.Config;
using System.Runtime.Caching;

namespace Owin.Security.Saml
{
    public class SamlAuthenticationOptions : AuthenticationOptions
    {
        // TODO: IDisposable...
        private readonly MemoryCache memoryCache = new MemoryCache("samlCache");
        public SamlAuthenticationOptions() : base("SAML2") {
            Description = new AuthenticationDescription
            {
                AuthenticationType = "SAML2",
                Caption = "Saml 2.0 Authentication protocol for OWIN"
            };
			SignInAsAuthenticationType = "SAML2";
            MetadataPath = "/saml2/metadata";
            LoginPath = "/saml2/login";
            LogoutPath = "/saml2/logout";
            GetFromCache = s => memoryCache.Get(s);
            SetInCache = (s, o, d) => memoryCache.Set(s, o, d);
        }

        /// <summary>
        /// SAML2 Configuration
        /// </summary>
        public Saml2Configuration Configuration { get; set; }

        /// <summary>
        /// Get from
        /// </summary>
        public Func<string, object> GetFromCache { get; set; }

        /// <summary>
        /// Defines login path for all bindings. Defaults to /saml2/login
        /// </summary>
        public string LoginPath { get; set; }

        /// <summary>
        /// Defines logout path for all bindings. Defaults to /saml2/logout
        /// </summary>
        public string LogoutPath { get; set; }
        /// <summary>
        /// Defines path used to acquire metadata. Defaults to /saml2/metadata
        /// </summary>
        public string MetadataPath { get; set; }
        public SamlAuthenticationNotifications Notifications { get; set; }
        /// <summary>
        /// Defines path used for redirection after a login occurs. This should be considered temporary
        /// until a better solution is developed for redirection to the original path
        /// </summary>
        public string RedirectAfterLogin { get; set; }

		/// <summary>
		/// The authentication type that will be used to sign in with. Typically this will be "ExternalCookie"
		/// to be picked up by the external cookie authentication middleware that persists the identity in a cookie.
		/// </summary>
		public string SignInAsAuthenticationType { get; set; }

		/// <summary>
		/// Passthrough property to Description.Caption.
		/// </summary>
		public string Caption
		{
			get
			{
				return Description.Caption;
			}
			set
			{
				Description.Caption = value;
			}
		}


        /// <summary>
        /// Session state (handy for validating responses are as expected in multi-server environments). Optional, default null
        /// </summary>
        public IDictionary<string, object> Session { get; set; }
        public Action<string, object, DateTime> SetInCache { get; set; }
    }
}
