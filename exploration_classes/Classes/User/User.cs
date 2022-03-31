using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Company;
using People;

namespace Users
{
    public class User
    {
        #region Constructors
        public User(string userName)
        {
            UserName = userName;
            id = new Guid();
            TimePoints = Convert.ToInt32(TimeSpan.FromDays(2).TotalSeconds);
        }

        [JsonConstructor]
        public User(string userName, Guid Id, Guid companyId, int timePoints)
        {
            UserName = userName;
            id = Id;
            CompanyId = companyId;
            TimePoints = timePoints;
        }
        #endregion

        #region Dictionaries and Properties
        public string UserName { get; set; }
        public Guid id { get; set; }
        public Guid CompanyId { get; set; }
        public int TimePoints { get; set; }
        #endregion

        #region Methods
        public void GainTimePoints(int timePoints)
        {
            int MaxTimePoints = Convert.ToInt32(TimeSpan.FromDays(4).TotalSeconds);
            if ((this.TimePoints + timePoints) > MaxTimePoints) this.TimePoints = MaxTimePoints;
            else this.TimePoints += timePoints;
        }
        public bool SpendTimePoints(int timePoints)
        {
            if (timePoints > this.TimePoints)
            {
                this.TimePoints -= timePoints;
                return true;
            }
            else return false;
        }
        #endregion
    }
}
