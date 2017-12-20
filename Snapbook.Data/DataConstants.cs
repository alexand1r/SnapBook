namespace Snapbook.Data
{
    public class DataConstants
    {
        public const int PhotoDescriptionMinLength = 3;
        public const int PhotoDescriptionMaxLength = 500;

        public const int CommentContentMinLength = 1;
        public const int CommentContentMaxLength = 300;

        public const int AdNameMinLength = 3;
        public const int AdNameMaxLength = 500;
        public const int AdDescriptionMinLength = 3;
        public const int AdDescriptionMaxLength = 1000;
        public const string AdWebsiteRegex = "www\\.[\\S]+\\.[\\S]+";

        public const int UserNameMinLength = 3;
        public const int UserNameMaxLength = 100;
        public const int UserBioMaxLength = 200;

        public const int AlbumTitleMinLength = 3;
        public const int AlbumTitleMaxLength = 30;
        public const int AlbumDescriptionMinLength = 3;
        public const int AlbumDescriptionMaxLength = 500;

        public const string StringLengthBetweenErrorMessage = "The {0} must be between {2} and {1} characters long.";
        public const string StringLengthMaxLengthErrorMessage = "The {0} must be no more than {1} characters long.";
        public const string WebsiteFormatErrorMessage = "Website field should be in format 'www.example.com'";
    }
}
