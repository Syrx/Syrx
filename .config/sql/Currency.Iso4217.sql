use [Syrx.Samples]
go

drop table if exists [Currency].[Iso4217]
go

drop schema if exists [Currency];
go

create schema [Currency] authorization [dbo];
go

create table [Currency].[Iso4217](
	 [code] varchar(3) not null
	,[numeric] varchar(3) not null
	,[decimals] int
	,[name] varchar(255)
)
go

;with [currencies](
	  [code]
	, [numeric]
	, [decimals]
	, [name]) as (
	select 'AED', '784', 2, 'United Arab Emirates dirham' union all
	select 'AFN', '971', 2, 'Afghan afghani' union all
	select 'ALL', '008', 2, 'Albanian lek' union all
	select 'AMD', '051', 2, 'Armenian dram' union all
	select 'ANG', '532', 2, 'Netherlands Antillean guilder' union all
	select 'AOA', '973', 2, 'Angolan kwanza' union all
	select 'ARS', '032', 2, 'Argentine peso' union all
	select 'AUD', '036', 2, 'Australian dollar' union all
	select 'AWG', '533', 2, 'Aruban florin' union all
	select 'AZN', '944', 2, 'Azerbaijani manat' union all
	select 'BAM', '977', 2, 'Bosnia and Herzegovina convertible mark' union all
	select 'BBD', '052', 2, 'Barbados dollar' union all
	select 'BDT', '050', 2, 'Bangladeshi taka' union all
	select 'BGN', '975', 2, 'Bulgarian lev' union all
	select 'BHD', '048', 3, 'Bahraini dinar' union all
	select 'BIF', '108', 0, 'Burundian franc' union all
	select 'BMD', '060', 2, 'Bermudian dollar' union all
	select 'BND', '096', 2, 'Brunei dollar' union all
	select 'BOB', '068', 2, 'Boliviano' union all
	select 'BOV', '984', 2, 'Bolivian Mvdol (funds code)' union all
	select 'BRL', '986', 2, 'Brazilian real' union all
	select 'BSD', '044', 2, 'Bahamian dollar' union all
	select 'BTN', '064', 2, 'Bhutanese ngultrum' union all
	select 'BWP', '072', 2, 'Botswana pula' union all
	select 'BYN', '933', 2, 'Belarusian ruble' union all
	select 'BZD', '084', 2, 'Belize dollar' union all
	select 'CAD', '124', 2, 'Canadian dollar' union all
	select 'CDF', '976', 2, 'Congolese franc' union all
	select 'CHE', '947', 2, 'WIR Euro (complementary currency)' union all
	select 'CHF', '756', 2, 'Swiss franc' union all
	select 'CHW', '948', 2, 'WIR Franc (complementary currency)' union all
	select 'CLF', '990', 4, 'Unidad de Fomento (funds code)' union all
	select 'CLP', '152', 0, 'Chilean peso' union all
	select 'CNY', '156', 2, 'Renminbi (Chinese) yuan' union all
	select 'COP', '170', 2, 'Colombian peso' union all
	select 'COU', '970', 2, 'Unidad de Valor Real (UVR) (funds code)' union all
	select 'CRC', '188', 2, 'Costa Rican colon' union all
	select 'CUC', '931', 2, 'Cuban convertible peso' union all
	select 'CUP', '192', 2, 'Cuban peso' union all
	select 'CVE', '132', 2, 'Cape Verdean escudo' union all
	select 'CZK', '203', 2, 'Czech koruna' union all
	select 'DJF', '262', 0, 'Djiboutian franc' union all
	select 'DKK', '208', 2, 'Danish krone' union all
	select 'DOP', '214', 2, 'Dominican peso' union all
	select 'DZD', '012', 2, 'Algerian dinar' union all
	select 'EGP', '818', 2, 'Egyptian pound' union all
	select 'ERN', '232', 2, 'Eritrean nakfa' union all
	select 'ETB', '230', 2, 'Ethiopian birr' union all
	select 'EUR', '978', 2, 'Euro' union all
	select 'FJD', '242', 2, 'Fiji dollar' union all
	select 'FKP', '238', 2, 'Falkland Islands pound' union all
	select 'GBP', '826', 2, 'Pound sterling' union all
	select 'GEL', '981', 2, 'Georgian lari' union all
	select 'GHS', '936', 2, 'Ghanaian cedi' union all
	select 'GIP', '292', 2, 'Gibraltar pound' union all
	select 'GMD', '270', 2, 'Gambian dalasi' union all
	select 'GNF', '324', 0, 'Guinean franc' union all
	select 'GTQ', '320', 2, 'Guatemalan quetzal' union all
	select 'GYD', '328', 2, 'Guyanese dollar' union all
	select 'HKD', '344', 2, 'Hong Kong dollar' union all
	select 'HNL', '340', 2, 'Honduran lempira' union all
	select 'HRK', '191', 2, 'Croatian kuna' union all
	select 'HTG', '332', 2, 'Haitian gourde' union all
	select 'HUF', '348', 2, 'Hungarian forint' union all
	select 'IDR', '360', 2, 'Indonesian rupiah' union all
	select 'ILS', '376', 2, 'Israeli new shekel' union all
	select 'INR', '356', 2, 'Indian rupee' union all
	select 'IQD', '368', 3, 'Iraqi dinar' union all
	select 'IRR', '364', 2, 'Iranian rial' union all
	select 'ISK', '352', 0, 'Icelandic króna' union all
	select 'JMD', '388', 2, 'Jamaican dollar' union all
	select 'JOD', '400', 3, 'Jordanian dinar' union all
	select 'JPY', '392', 0, 'Japanese yen' union all
	select 'KES', '404', 2, 'Kenyan shilling' union all
	select 'KGS', '417', 2, 'Kyrgyzstani som' union all
	select 'KHR', '116', 2, 'Cambodian riel' union all
	select 'KMF', '174', 0, 'Comoro franc' union all
	select 'KPW', '408', 2, 'North Korean won' union all
	select 'KRW', '410', 0, 'South Korean won' union all
	select 'KWD', '414', 3, 'Kuwaiti dinar' union all
	select 'KYD', '136', 2, 'Cayman Islands dollar' union all
	select 'KZT', '398', 2, 'Kazakhstani tenge' union all
	select 'LAK', '418', 2, 'Lao kip' union all
	select 'LBP', '422', 2, 'Lebanese pound' union all
	select 'LKR', '144', 2, 'Sri Lankan rupee' union all
	select 'LRD', '430', 2, 'Liberian dollar' union all
	select 'LSL', '426', 2, 'Lesotho loti' union all
	select 'LYD', '434', 3, 'Libyan dinar' union all
	select 'MAD', '504', 2, 'Moroccan dirham' union all
	select 'MDL', '498', 2, 'Moldovan leu' union all
	select 'MGA', '969', 2, 'Malagasy ariary' union all
	select 'MKD', '807', 2, 'Macedonian denar' union all
	select 'MMK', '104', 2, 'Myanmar kyat' union all
	select 'MNT', '496', 2, 'Mongolian tögrög' union all
	select 'MOP', '446', 2, 'Macanese pataca' union all
	select 'MRU', '929', 2, 'Mauritanian ouguiya' union all
	select 'MUR', '480', 2, 'Mauritian rupee' union all
	select 'MVR', '462', 2, 'Maldivian rufiyaa' union all
	select 'MWK', '454', 2, 'Malawian kwacha' union all
	select 'MXN', '484', 2, 'Mexican peso' union all
	select 'MXV', '979', 2, 'Mexican Unidad de Inversion (UDI) (funds code)' union all
	select 'MYR', '458', 2, 'Malaysian ringgit' union all
	select 'MZN', '943', 2, 'Mozambican metical' union all
	select 'NAD', '516', 2, 'Namibian dollar' union all
	select 'NGN', '566', 2, 'Nigerian naira' union all
	select 'NIO', '558', 2, 'Nicaraguan córdoba' union all
	select 'NOK', '578', 2, 'Norwegian krone' union all
	select 'NPR', '524', 2, 'Nepalese rupee' union all
	select 'NZD', '554', 2, 'New Zealand dollar' union all
	select 'OMR', '512', 3, 'Omani rial' union all
	select 'PAB', '590', 2, 'Panamanian balboa' union all
	select 'PEN', '604', 2, 'Peruvian sol' union all
	select 'PGK', '598', 2, 'Papua New Guinean kina' union all
	select 'PHP', '608', 2, 'Philippine peso' union all
	select 'PKR', '586', 2, 'Pakistani rupee' union all
	select 'PLN', '985', 2, 'Polish złoty' union all
	select 'PYG', '600', 0, 'Paraguayan guaraní' union all
	select 'QAR', '634', 2, 'Qatari riyal' union all
	select 'RON', '946', 2, 'Romanian leu' union all
	select 'RSD', '941', 2, 'Serbian dinar' union all
	select 'RUB', '643', 2, 'Russian ruble' union all
	select 'RWF', '646', 0, 'Rwandan franc' union all
	select 'SAR', '682', 2, 'Saudi riyal' union all
	select 'SBD', '090', 2, 'Solomon Islands dollar' union all
	select 'SCR', '690', 2, 'Seychelles rupee' union all
	select 'SDG', '938', 2, 'Sudanese pound' union all
	select 'SEK', '752', 2, 'Swedish krona/kronor' union all
	select 'SGD', '702', 2, 'Singapore dollar' union all
	select 'SHP', '654', 2, 'Saint Helena pound' union all
	select 'SLL', '694', 2, 'Sierra Leonean leone' union all
	select 'SOS', '706', 2, 'Somali shilling' union all
	select 'SRD', '968', 2, 'Surinamese dollar' union all
	select 'SSP', '728', 2, 'South Sudanese pound' union all
	select 'STN', '930', 2, 'São Tomé and Príncipe dobra' union all
	select 'SVC', '222', 2, 'Salvadoran colón' union all
	select 'SYP', '760', 2, 'Syrian pound' union all
	select 'SZL', '748', 2, 'Swazi lilangeni' union all
	select 'THB', '764', 2, 'Thai baht' union all
	select 'TJS', '972', 2, 'Tajikistani somoni' union all
	select 'TMT', '934', 2, 'Turkmenistan manat' union all
	select 'TND', '788', 3, 'Tunisian dinar' union all
	select 'TOP', '776', 2, 'Tongan paʻanga' union all
	select 'TRY', '949', 2, 'Turkish lira' union all
	select 'TTD', '780', 2, 'Trinidad and Tobago dollar' union all
	select 'TWD', '901', 2, 'New Taiwan dollar' union all
	select 'TZS', '834', 2, 'Tanzanian shilling' union all
	select 'UAH', '980', 2, 'Ukrainian hryvnia' union all
	select 'UGX', '800', 0, 'Ugandan shilling' union all
	select 'USD', '840', 2, 'United States dollar' union all
	select 'USN', '997', 2, 'United States dollar (next day) (funds code)' union all
	select 'UYI', '940', 0, 'Uruguay Peso en Unidades Indexadas (URUIURUI) (funds code)' union all
	select 'UYU', '858', 2, 'Uruguayan peso' union all
	select 'UYW', '927', 4, 'Unidad previsional' union all
	select 'UZS', '860', 2, 'Uzbekistan som' union all
	select 'VES', '928', 2, 'Venezuelan bolívar soberano' union all
	select 'VND', '704', 0, 'Vietnamese đồng' union all
	select 'VUV', '548', 0, 'Vanuatu vatu' union all
	select 'WST', '882', 2, 'Samoan tala' union all
	select 'XAF', '950', 0, 'CFA franc BEAC' union all
	select 'XAG', '961', null, 'Silver (one troy ounce)' union all
	select 'XAU', '959', null, 'Gold (one troy ounce)' union all
	select 'XBA', '955', null, 'European Composite Unit (EURCO) (bond market unit)' union all
	select 'XBB', '956', null, 'European Monetary Unit (E.M.U.-6) (bond market unit)' union all
	select 'XBC', '957', null, 'European Unit of Account 9 (E.U.A.-9) (bond market unit)' union all
	select 'XBD', '958', null, 'European Unit of Account 17 (E.U.A.-17) (bond market unit)' union all
	select 'XCD', '951', 2, 'East Caribbean dollar' union all
	select 'XDR', '960', null, 'Special drawing rights' union all
	select 'XOF', '952', 0, 'CFA franc BCEAO' union all
	select 'XPD', '964', null, 'Palladium (one troy ounce)' union all
	select 'XPF', '953', 0, 'CFP franc (franc Pacifique)' union all
	select 'XPT', '962', null, 'Platinum (one troy ounce)' union all
	select 'XSU', '994', null, 'SUCRE' union all
	select 'XTS', '963', null, 'Code reserved for testing' union all
	select 'XUA', '965', null, 'ADB Unit of Account' union all
	select 'XXX', '999', null, 'No currency' union all
	select 'YER', '886', 2, 'Yemeni rial' union all
	select 'ZAR', '710', 2, 'South African rand' union all
	select 'ZMW', '967', 2, 'Zambian kwacha' union all
	select 'ZWL', '932', 2, 'Zimbabwean dollar' )
insert into [Currency].[Iso4217] (
	  [code]
	, [numeric]
	, [decimals]
	, [name])
select [code]
	, [numeric]
	, [decimals]
	, [name] 
from [currencies]
go

select [code]
	, [numeric]
	, [decimals]
	, [name] 
from [Currency].[Iso4217];