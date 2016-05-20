using System;

namespace DynamicSurvey.Core.Entities
{
    public class UserRight
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual DateTime LastModified { get; set; }
    }
}
