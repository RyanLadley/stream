using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.core.Models.DataTransfer.Response
{
    public class UserSettingsResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long TotalSpacedUsed { get; set; }
        public long TotalSpacedAllowed { get; set; }

        public static UserSettingsResponse MapFromObject(User user, long spaceUsed)
        {
            return new UserSettingsResponse
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.LastName,
                TotalSpacedUsed = spaceUsed,
                TotalSpacedAllowed = 10737418240
            };
        }
    }
}
