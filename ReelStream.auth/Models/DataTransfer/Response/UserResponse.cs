using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.auth.Models.DataTransfer.Response
{
    public class UserResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public static UserResponse MapFromObject(User user)
        {
            var response = new UserResponse()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return response;
        }
    }
}
