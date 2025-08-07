namespace SRH.ValueObjects.Currency;

public readonly partial record struct CurrencyInfo
{
    // ISO 4217    
    public static readonly CurrencyInfo Empty = new("", "", "", "", 0);
    public static readonly CurrencyInfo IranRial = new("IRR", "IRAN, ISLAMIC REPUBLIC OF", "ریال ایران", "ريال", 0);
    public static readonly CurrencyInfo USDollar = new("USD", "UNITED STATES", "دلار آمریکا", "$", 2);
    public static readonly CurrencyInfo Euro = new("EUR", "Euro", "یورو", "€", 2);
    public static readonly CurrencyInfo British_Pound_Sterling = new("GBP", "British Pound Sterling", "پوند بریتانیا", "£", 2);
    public static readonly CurrencyInfo Japanese_Yen = new("JPY", "Japanese Yen", "ین ژاپن", "¥", 0);
    public static readonly CurrencyInfo Indian_Rupee = new("INR", "Indian Rupee", "روپیه هند", "₹", 2);
    public static readonly CurrencyInfo Swiss_Franc = new("CHF", "Swiss Franc", "فرانک سوئیس", "CHF", 2);
    public static readonly CurrencyInfo Canadian_Dollar = new("CAD", "Canadian Dollar", "دلار کانادا", "C$", 2);
    public static readonly CurrencyInfo Australian_Dollar = new("AUD", "Australian Dollar", "دلار استرالیا", "A$", 2);
    public static readonly CurrencyInfo Chinese_Yuan_Renminbi = new("CNY", "Chinese Yuan Renminbi", "یوان چین", "元 / ¥", 2);
    public static readonly CurrencyInfo United_Arab_Emirates_Dirham = new("AED", "United Arab Emirates Dirham", "درهم امارات متحده عربی", "د.إ", 2);
    public static readonly CurrencyInfo Afghan_Afghani = new("AFN", "Afghan Afghani", "افغانی افغانستان", "AFN", 2);
    public static readonly CurrencyInfo Albanian_Lek = new("ALL", "Albanian Lek", "لک آلبانی", "ALL", 2);
    public static readonly CurrencyInfo Armenian_Dram = new("AMD", "Armenian Dram", "درام ارمنستان", "AMD", 2);
    public static readonly CurrencyInfo Netherlands_Antillean_Guilder = new("ANG", "Netherlands Antillean Guilder", "گیلدر آنتیل هلند", "ANG", 2);
    public static readonly CurrencyInfo Angolan_Kwanza = new("AOA", "Angolan Kwanza", "کوانزا آنگولا", "AOA", 2);
    public static readonly CurrencyInfo Argentine_Peso = new("ARS", "Argentine Peso", "پزو آرژانتین", "ARS", 2);
    public static readonly CurrencyInfo Aruban_Florin = new("AWG", "Aruban Florin", "فلورین آروبا", "AWG", 2);
    public static readonly CurrencyInfo Azerbaijani_Manat = new("AZN", "Azerbaijani Manat", "منات آذربایجان", "AZN", 2);
    public static readonly CurrencyInfo Bosnia_and_Herzegovina_Convertible_Mark = new("BAM", "Bosnia and Herzegovina Convertible Mark", "مارک تبدیل‌پذیر بوسنی و هرزگوین", "BAM", 2);
    public static readonly CurrencyInfo Barbadian_Dollar = new("BBD", "Barbadian Dollar", "دلار باربادوس", "BBD", 2);
    public static readonly CurrencyInfo Bangladeshi_Taka = new("BDT", "Bangladeshi Taka", "تاکای بنگلادش", "BDT", 2);
    public static readonly CurrencyInfo Bulgarian_Lev = new("BGN", "Bulgarian Lev", "لوا بلغارستان", "BGN", 2);
    public static readonly CurrencyInfo Bahraini_Dinar = new("BHD", "Bahraini Dinar", "دینار بحرین", "BHD", 3);
    public static readonly CurrencyInfo Burundian_Franc = new("BIF", "Burundian Franc", "فرانک بوروندی", "BIF", 0);
    public static readonly CurrencyInfo Bermudian_Dollar = new("BMD", "Bermudian Dollar", "دلار برمودا", "BMD", 2);
    public static readonly CurrencyInfo Brunei_Dollar = new("BND", "Brunei Dollar", "دلار برونئی", "BND", 2);
    public static readonly CurrencyInfo Bolivian_Boliviano = new("BOB", "Bolivian Boliviano", "بولیویانو بولیوی", "BOB", 2);
    public static readonly CurrencyInfo Brazilian_Real = new("BRL", "Brazilian Real", "رئال برزیل", "BRL", 2);
    public static readonly CurrencyInfo Bahamian_Dollar = new("BSD", "Bahamian Dollar", "دلار باهاما", "BSD", 2);
    public static readonly CurrencyInfo Bhutanese_Ngultrum = new("BTN", "Bhutanese Ngultrum", "نگولتروم بوتان", "BTN", 2);
    public static readonly CurrencyInfo Botswana_Pula = new("BWP", "Botswana Pula", "پولا بوتسوانا", "BWP", 2);
    public static readonly CurrencyInfo Belarusian_Ruble = new("BYN", "Belarusian Ruble", "روبل بلاروس", "BYN", 2);
    public static readonly CurrencyInfo Belize_Dollar = new("BZD", "Belize Dollar", "دلار بلیز", "BZD", 2);
    public static readonly CurrencyInfo Congolese_Franc = new("CDF", "Congolese Franc", "فرانک کنگو", "CDF", 2);
    public static readonly CurrencyInfo WIR_Euro = new("CHE", "WIR Euro", "یورو وی‌آی‌آر", "CHE", 2);
    public static readonly CurrencyInfo WIR_Franc = new("CHW", "WIR Franc", "فرانک وی‌آی‌آر", "CHW", 2);
    public static readonly CurrencyInfo Chilean_Peso = new("CLP", "Chilean Peso", "پزو شیلی", "CLP", 0);
    public static readonly CurrencyInfo Colombian_Peso = new("COP", "Colombian Peso", "پزو کلمبیا", "COP", 2);
    public static readonly CurrencyInfo Costa_Rican_Colón = new("CRC", "Costa Rican Colón", "کولون کاستاریکا", "CRC", 2);
    public static readonly CurrencyInfo Cuban_Peso = new("CUP", "Cuban Peso", "پزو کوبا", "CUP", 2);
    public static readonly CurrencyInfo Cape_Verdean_Escudo = new("CVE", "Cape Verdean Escudo", "اسکودو دماغه سبز", "CVE", 2);
    public static readonly CurrencyInfo Czech_Koruna = new("CZK", "Czech Koruna", "کرونای چک", "CZK", 2);
    public static readonly CurrencyInfo Djiboutian_Franc = new("DJF", "Djiboutian Franc", "فرانک جیبوتی", "DJF", 0);
    public static readonly CurrencyInfo Danish_Krone = new("DKK", "Danish Krone", "کرون دانمارک", "DKK", 2);
    public static readonly CurrencyInfo Dominican_Peso = new("DOP", "Dominican Peso", "پزو دومینیکن", "DOP", 2);
    public static readonly CurrencyInfo Algerian_Dinar = new("DZD", "Algerian Dinar", "دینار الجزایر", "DZD", 2);
    public static readonly CurrencyInfo Egyptian_Pound = new("EGP", "Egyptian Pound", "پوند مصر", "EGP", 2);
    public static readonly CurrencyInfo Eritrean_Nakfa = new("ERN", "Eritrean Nakfa", "ناکفای اریتره", "ERN", 2);
    public static readonly CurrencyInfo Ethiopian_Birr = new("ETB", "Ethiopian Birr", "بیر اتیوپی", "ETB", 2);
    public static readonly CurrencyInfo Fijian_Dollar = new("FJD", "Fijian Dollar", "دلار فیجی", "FJD", 2);
    public static readonly CurrencyInfo Falkland_Islands_Pound = new("FKP", "Falkland Islands Pound", "پوند جزایر فالکلند", "FKP", 2);
    public static readonly CurrencyInfo Faroese_Króna = new("FOK", "Faroese Króna", "کرونای جزایر فارو", "FOK", 2);
    public static readonly CurrencyInfo Georgian_Lari = new("GEL", "Georgian Lari", "لاری گرجستان", "GEL", 2);
    public static readonly CurrencyInfo Guernsey_Pound = new("GGP", "Guernsey Pound", "پوند گرنزی", "GGP", 2);
    public static readonly CurrencyInfo Ghanaian_Cedi = new("GHS", "Ghanaian Cedi", "سدی غنا", "GHS", 2);
    public static readonly CurrencyInfo Gibraltar_Pound = new("GIP", "Gibraltar Pound", "پوند جبل‌الطارق", "GIP", 2);
    public static readonly CurrencyInfo Gambian_Dalasi = new("GMD", "Gambian Dalasi", "دالاسی گامبیا", "GMD", 2);
    public static readonly CurrencyInfo Guinean_Franc = new("GNF", "Guinean Franc", "فرانک گینه", "GNF", 0);
    public static readonly CurrencyInfo Guatemalan_Quetzal = new("GTQ", "Guatemalan Quetzal", "کتزال گواتمالا", "GTQ", 2);
    public static readonly CurrencyInfo Guyanese_Dollar = new("GYD", "Guyanese Dollar", "دلار گویان", "GYD", 2);
    public static readonly CurrencyInfo Hong_Kong_Dollar = new("HKD", "Hong Kong Dollar", "دلار هنگ‌کنگ", "HKD", 2);
    public static readonly CurrencyInfo Honduran_Lempira = new("HNL", "Honduran Lempira", "لمپیرای هندوراس", "HNL", 2);
    public static readonly CurrencyInfo Croatian_Kuna = new("HRK", "Croatian Kuna", "کونا کرواسی", "HRK", 2);
    public static readonly CurrencyInfo Haitian_Gourde = new("HTG", "Haitian Gourde", "گورد هائیتی", "HTG", 2);
    public static readonly CurrencyInfo Hungarian_Forint = new("HUF", "Hungarian Forint", "فورینت مجارستان", "HUF", 2);
    public static readonly CurrencyInfo Indonesian_Rupiah = new("IDR", "Indonesian Rupiah", "روپیه اندونزی", "IDR", 2);
    public static readonly CurrencyInfo Israeli_New_Shekel = new("ILS", "Israeli New Shekel", "شیکل جدید اسرائیل", "ILS", 2);
    public static readonly CurrencyInfo Isle_of_Man_Pound = new("IMP", "Isle of Man Pound", "پوند جزیره من", "IMP", 2);
    public static readonly CurrencyInfo Iraqi_Dinar = new("IQD", "Iraqi Dinar", "دینار عراق", "IQD", 3);
    public static readonly CurrencyInfo Icelandic_Króna = new("ISK", "Icelandic Króna", "کرونای ایسلند", "ISK", 0);
    public static readonly CurrencyInfo Jersey_Pound = new("JEP", "Jersey Pound", "پوند جرزی", "JEP", 2);
    public static readonly CurrencyInfo Jamaican_Dollar = new("JMD", "Jamaican Dollar", "دلار جامائیکا", "JMD", 2);
    public static readonly CurrencyInfo Jordanian_Dinar = new("JOD", "Jordanian Dinar", "دینار اردن", "JOD", 3);
    public static readonly CurrencyInfo Kenyan_Shilling = new("KES", "Kenyan Shilling", "شیلینگ کنیا", "KES", 2);
    public static readonly CurrencyInfo Kyrgyzstani_Som = new("KGS", "Kyrgyzstani Som", "سام قرقیزستان", "KGS", 2);
    public static readonly CurrencyInfo Cambodian_Riel = new("KHR", "Cambodian Riel", "ریل کامبوج", "KHR", 2);

    private static Dictionary<string, CurrencyInfo> _allCurrencies = Generate();

    public static CurrencyInfo FromCode(string? code)
    {
        if (code is null) return Empty;

        if (_allCurrencies.TryGetValue(code, out var result)) return result;

        return Empty;
    }
    public static IReadOnlyCollection<CurrencyInfo> AllCurrencies => _allCurrencies.Where(x => !string.IsNullOrWhiteSpace(x.Value.Code)).Select(c => c.Value).ToList().AsReadOnly();

    private static Dictionary<string, CurrencyInfo> Generate()
    {
        var fieldsOfType = TypeHelpers.GetAllStaticFieldsOfType<CurrencyInfo>().ToList();
        return fieldsOfType.ToDictionary(x => x.Code, x => x, StringComparer.InvariantCultureIgnoreCase);
    }
    private static IEnumerable<CurrencyInfo> GetAllStaticFieldsOfType()
    {
        var enumerationType = typeof(CurrencyInfo);

        return enumerationType
            .GetFields(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.FlattenHierarchy
            ).Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fielInfo => (CurrencyInfo)fielInfo.GetValue(default)!);
    }
    public PrimitiveResult<CurrencyInfo> Validate()
    {
        return this.Equals(Empty)
            ? PrimitiveResult.Failure<CurrencyInfo>("", "کد ارز اشتباه است")
            : this;

    }
}
