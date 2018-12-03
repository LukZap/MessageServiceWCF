using System.Runtime.Serialization;

namespace MessageService.Enums
{
    [DataContract]
    public enum ContactType
    {
        Mobile,

        Fax,

        Email,

        OfficePhone,

        OfficeFax,

        OfficeEmail
    }
}