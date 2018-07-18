using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ScaleModelsStore.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(100)]
        public string Address { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("LastName", this.LastName));
            userIdentity.AddClaim(new Claim("FirstName", this.FirstName));
            userIdentity.AddClaim(new Claim("Email", this.Email));
            userIdentity.AddClaim(new Claim("PostalCode", String.IsNullOrEmpty(this.PostalCode)?"":this.PostalCode));
            userIdentity.AddClaim(new Claim("Country", String.IsNullOrEmpty(this.Country) ? "" : this.Country));
            userIdentity.AddClaim(new Claim("City", String.IsNullOrEmpty(this.City) ? "" : this.City));
            userIdentity.AddClaim(new Claim("Address", String.IsNullOrEmpty(this.Address) ? "" : this.Address));
            userIdentity.AddClaim(new Claim("PhoneNumber", String.IsNullOrEmpty(this.PhoneNumber) ? "" : this.PhoneNumber));
            return userIdentity;
        }

        public class IdentityLoginConfig:EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityLoginConfig()
            {
                HasKey(ilc => ilc.UserId);
            }
        }

        public class IdentityRoleConfig:EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityRoleConfig()
            {
                HasKey(irc => irc.RoleId);
            }
        }
    }
}