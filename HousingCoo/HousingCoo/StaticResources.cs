namespace HousingCoo {
    public static class StaticResources {
        static StaticResources() {
            Icons = new Icon();
        }

        public static Icon Icons { get; }

        public class Icon {
            public readonly string ApplicationIcon = "ic_person_black_18dp.png";
            public readonly string PersonBlack = "ic_person_black_18dp.png";
            public readonly string MessageBlack = "ic_person_black_18dp.png";
            public readonly string MessageWhite = "ic_message_white_24dp.png";
            public readonly string PeopleWhite = "ic_people_white_24dp.png";
            public readonly string PublicWhite = "ic_public_white_24dp.png";
            public readonly string ErrorWhite = "ic_error_white_24dp.png";
            public readonly string HomeWhite = "ic_home_white_24dp.png";
            public readonly string StarWhite = "ic_star_white_24dp.png";
        }
    }
}
