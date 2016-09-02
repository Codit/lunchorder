namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents a username / id for a user
    /// </summary>
    public class SimpleUser
    {
        private string _fullName;
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(_fullName) ? UserName : _fullName;
            }
            set { _fullName = value; }
        }
    }
}