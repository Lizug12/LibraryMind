using LibraryMind.Models;
using System;
using System.Linq;

namespace LibraryMind.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }

            var books = new Books[]
            {//25
            new Books{BookName="1793. История одного убийства",Author="Никлас Натт-о-Даг",Genre="Детектив",Pages=400,YearPublishing=DateTime.Parse("09-10-2011")},
            new Books{BookName="Тревожные люди",Author="Фредрик Бакман",Genre="Современная литература",Pages=300,YearPublishing=DateTime.Parse("04-11-2019")},
            new Books{BookName="Вторая жизнь Уве",Author="Фредрик Бакман",Genre="Современная литература",Pages=330,YearPublishing=DateTime.Parse("29-08-2012")},
            new Books{BookName="Мы против вас",Author="Фредрик Бакман",Genre="Современная литература",Pages=440,YearPublishing=DateTime.Parse("05-12-2017")},
            new Books{BookName="Эшелон на Самарканд",Author="Гузель Яхина",Genre="Историческая литература",Pages=480,YearPublishing=DateTime.Parse("23-03-2021")},
            new Books{BookName="Хочу и буду: Принять себя, полюбить жизнь и стать счастливым",Author="Михаил Лабковский",Genre="Психология",Pages=220,YearPublishing=DateTime.Parse("29-08-2017")},
            new Books{BookName="Русское",Author="Эдвард Резерфорд",Genre="Историческая литература",Pages=1400,YearPublishing=DateTime.Parse("18-05-1991")},
            new Books{BookName="Станционный смотритель. Долг платежом красен",Author="Григорий Шаргородский",Genre="Фэнтези",Pages=330,YearPublishing=DateTime.Parse("17-05-2021")},
            new Books{BookName="Станционный смотритель. Незнамо куда",Author="Григорий Шаргородский",Genre="Фэнтези",Pages=320,YearPublishing=DateTime.Parse("01-10-2020")},
            new Books{BookName="Станционный смотритель. Бес в ребро",Author="Григорий Шаргородский",Genre="Фэнтези",Pages=310,YearPublishing=DateTime.Parse("21-01-2020")},
            new Books{BookName="Тёмные пути",Author="Андрей Васильев",Genre="Фэнтези",Pages=350,YearPublishing=DateTime.Parse("29-04-2021")},
            new Books{BookName="Любовь к себе. 50 способов повысить самооценку",Author="Анастасия Залога",Genre="Психология",Pages=200,YearPublishing=DateTime.Parse("26-09-2019")},
            new Books{BookName="Безмолвный пациент",Author="Алекс Михаэлидес",Genre="Трилер",Pages=290,YearPublishing=DateTime.Parse("27-02-2019")},
            new Books{BookName="Ржаной хлеб. Азбука пекаря",Author="Сергей Кириллов",Genre="Кулинария",Pages=194,YearPublishing=DateTime.Parse("21-02-2020")},
            new Books{BookName="Меню недели. Тайм-менеджмент на кухне",Author="Дарья Черненко",Genre="Кулинария",Pages=280,YearPublishing=DateTime.Parse("15-04-2021")},
            new Books{BookName="Мрачный залив",Author="Рейчел Кейн",Genre="Триллер",Pages=400,YearPublishing=DateTime.Parse("13-05-2021")},
            new Books{BookName="Ходячий замок",Author="Диана Уинн Джонс",Genre="Сказка",Pages=280,YearPublishing=DateTime.Parse("05-03-1986")},
            new Books{BookName="Малыш и Карлсон, который живет на крыше",Author="Астрид Линдгрен",Genre="Сказки",Pages=120,YearPublishing=DateTime.Parse("01-11-1955")},
            new Books{BookName="Собачье сердце",Author="Михаил Булгаков",Genre="Фантастика",Pages=110,YearPublishing=DateTime.Parse("25-05-1925")},
            new Books{BookName="Мастер и Маргарита",Author="Михаил Булгаков",Genre="Фантастика",Pages=530,YearPublishing=DateTime.Parse("23-06-2017")},
            new Books{BookName="Сестра четырех",Author="Евгений Водолазкин",Genre="Пьеса",Pages=190,YearPublishing=DateTime.Parse("18-05-2020")},
            new Books{BookName="Жареные зеленые помидоры в кафе «Полустанок»",Author="Фэнни Флэгг",Genre="Классика",Pages=320,YearPublishing=DateTime.Parse("04-10-1987")},
            new Books{BookName="Ход королевы",Author="Уолтер Тевис",Genre="Классика",Pages=390,YearPublishing=DateTime.Parse("13-04-1983")},
            new Books{BookName="Институт",Author="Стивен Кинг",Genre="Ужасы",Pages=580,YearPublishing=DateTime.Parse("20-04-2019")},
            new Books{BookName="Будет кровь",Author="Стивен Кинг",Genre="Ужасы",Pages=490,YearPublishing=DateTime.Parse("03-12-2020")}
            };
            foreach (Books b in books)
            {
                context.Book.Add(b);
            }
            context.SaveChanges();

            var readers = new Readers[]
            {//30
            new Readers{FIO="Булгаков Владимир Максимович",Address="392003, г. Назрань, ул. Малоохтинский пр-кт, дом 163, квартира 882",Phone="+7 (943) 633-83-74",Age=35},
            new Readers{FIO="Егоров Ульян Артемович",Address="243415, г. Заостровье, ул. Чапыгина, дом 57, квартира 164",Phone="+7 (944) 321-90-05",Age=24},
            new Readers{FIO="Минаева Рогнеда Евгеньевна",Address="368808, г. Обливская, ул. Верхняя аллея, дом 116, квартира 61",Phone="+7 (906) 113-02-08",Age=44},
            new Readers{FIO="Городнов Федосий Алексеевич",Address="186806, г. Куйбышевское, ул. Николощеповский 2-й пер, дом 123, квартира 966",Phone="+7 (931) 876-80-60",Age=35},
            new Readers{FIO="Недашковский Лукьян Георгиевич",Address="692239, г. Карабаш, ул. Проектируемый 1087-й проезд, дом 158, квартира 190",Phone="+7 (943) 001-47-56",Age=49},
            new Readers{FIO="Радионов Арнольд Юрьевич",Address="157345, г. Грайворон, ул. Ярмарка Восточная тер, дом 159, квартира 486",Phone="+7 (975) 781-09-98",Age=26},
            new Readers{FIO="Репина Алина Анатольевна",Address="678314, г. Сыктывкар, ул. Остафьевская, дом 123, квартира 83",Phone="+7 (972) 966-23-16",Age=23},
            new Readers{FIO="Фомин Даниил Иванович",Address="429386, г. Уват, ул. Станичная, дом 69, квартира 270",Phone="+7 (917) 280-50-70",Age=41},
            new Readers{FIO="Шмидт Каллистрат Геннадиевич",Address="356170, г. Малые Горки, ул. Строителей, дом 163, квартира 512",Phone="+7 (928) 967-56-40",Age=51},
            new Readers{FIO="Путина Галя Аркадьевна",Address="164023, г. Гатчина, ул. Лазарева, дом 170, квартира 515",Phone="+7 (983) 620-83-23",Age=34},
            new Readers{FIO="Сергеева Оксана Иосифовна",Address="618716, г. Мурманск, ул. Дубленский пер, дом 166, квартира 494",Phone="+7 (902) 366-89-78",Age=48},
            new Readers{FIO="Максимчук Домна Александровна",Address="427923, г. Учалы, ул. Академика Ильюшина, дом 69, квартира 869",Phone="+7 (971) 067-96-44",Age=29},
            new Readers{FIO="Белоусов Елисей Данилович",Address="368456, г. Учкекен, ул. Вокзальный проезд, дом 115, квартира 694",Phone="+7 (930) 740-22-12",Age=34},
            new Readers{FIO="Егорова Неонила Павловна",Address="161413, г. Черный Яр, ул. Лучевой 3-й просек, дом 103, квартира 682",Phone="+7 (961) 008-49-77",Age=35},
            new Readers{FIO="Миллер Ярополк Вадимович",Address="152254, г. Таловая, ул. Заневка д, дом 143, квартира 32",Phone="+7 (921) 643-81-73",Age=47},
            new Readers{FIO="Дидиченко Евдокия Филипповна",Address="301446, г. Лоухи, ул. Петровско-Разумовская аллея, дом 148, квартира 964",Phone="+7 (902) 100-97-02",Age=49},
            new Readers{FIO="Лермонтов Антон Станиславович",Address="673498, г. Грибки, ул. Дурова, дом 35, квартира 910",Phone="+7 (974) 035-23-18",Age=27},
            new Readers{FIO="Титов Григорий Кириллович",Address="152042, г. Ведено, ул. Чоботовский проезд, дом 180, квартира 945",Phone="+7 (916) 521-32-75",Age=18},
            new Readers{FIO="Городнова Лиана Матвеевна",Address="155801, г. Ясное, ул. Газетный пер, дом 13, квартира 54",Phone="+7 (922) 533-61-55",Age=26},
            new Readers{FIO="Высоцкий Денис Русланович",Address="452634, г. Заречный, ул. Волжский б-р, дом 157, квартира 98",Phone="+7 (973) 772-64-12",Age=21},
            new Readers{FIO="Соболев Пахом Тарасович",Address="307211, г. Хандыга, ул. ГСК Колесо(Пермитина-Ватутина-Выставочна, дом 33, квартира 224",Phone="+7 (915) 788-17-43",Age=36},
            new Readers{FIO="Беляков Евграф Павлович",Address="353835, г. Шилово, ул. Свердловский сад, дом 78, квартира 588",Phone="+7 (991) 578-63-80",Age=46},
            new Readers{FIO="Волочкова Ева Алексеевна",Address="366020, г. Вохма, ул. Миллионная, дом 89, квартира 87",Phone="+7 (970) 092-91-10",Age=24},
            new Readers{FIO="Волочкова Клара Александровна",Address="671640, г. Малмыж, ул. Тушинская, дом 159, квартира 736",Phone="+7 (953) 445-43-66",Age=27},
            new Readers{FIO="Баранов Максимильян Вячеславович",Address="453695, г. Таврическое, ул. Непокоренных пр-кт, дом 150, квартира 180",Phone="+7 (946) 986-86-02",Age=33},
            new Readers{FIO="Меньшов Дмитрий Павлович",Address="172123, г. Донской, ул. Чуйская, дом 87, квартира 246",Phone="+7 (948) 624-07-20",Age=17},
            new Readers{FIO="Лермонтов Артем Тимурович",Address="422130, г. Исса, ул. Центральный Хорошевского Серебряного Бор проезд, дом 177, квартира 465",Phone="+7 (984) 292-24-69",Age=24},
            new Readers{FIO="Рустамова Надежда Матвеевна",Address="396605, г. Алексеевка, ул. Теплоходная, дом 185, квартира 273",Phone="+7 (934) 594-77-43",Age=35},
            new Readers{FIO="Зайцев Евгений Ильич",Address="673413, г. Аромашево, ул. Сивашский 2-й пер, дом 51, квартира 57",Phone="+7 (997) 059-46-41",Age=37},
            new Readers{FIO="Зайцев Савватий Васильевич",Address="612011, г. Совозное, ул. Таежная, дом 63, квартира 512",Phone="+7 (940) 166-53-61",Age=34}
            };
            foreach (Readers r in readers)
            {
                context.Reader.Add(r);
            }
            context.SaveChanges();

            var lendings = new Lendings[]
            {
            new Lendings{ReaderID=1,BookID=2,LendingDate=DateTime.Parse("01-01-2021"),ReturnDate=DateTime.Parse("11-01-2021")},
            new Lendings{ReaderID=2,BookID=1,LendingDate=DateTime.Parse("02-01-2021"),ReturnDate=DateTime.Parse("12-01-2021")},
            new Lendings{ReaderID=3,BookID=12,LendingDate=DateTime.Parse("11-02-2021"),ReturnDate=DateTime.Parse("15-02-2021")},
            new Lendings{ReaderID=4,BookID=11,LendingDate=DateTime.Parse("09-01-2021"),ReturnDate=DateTime.Parse("18-01-2021")},
            new Lendings{ReaderID=4,BookID=2,LendingDate=DateTime.Parse("16-01-2021"),ReturnDate=DateTime.Parse("23-01-2021")},
            new Lendings{ReaderID=5,BookID=5,LendingDate=DateTime.Parse("18-01-2021"),ReturnDate=DateTime.Parse("25-01-2021")},
            new Lendings{ReaderID=6,BookID=6,LendingDate=DateTime.Parse("05-01-2021"),ReturnDate=DateTime.Parse("18-01-2021")},
            new Lendings{ReaderID=6,BookID=7,LendingDate=DateTime.Parse("05-01-2021"),ReturnDate=DateTime.Parse("24-01-2021")},
            new Lendings{ReaderID=7,BookID=23,LendingDate=DateTime.Parse("26-02-2021"),ReturnDate=DateTime.Parse("04-03-2021")},
            new Lendings{ReaderID=8,BookID=17,LendingDate=DateTime.Parse("17-02-2021"),ReturnDate=DateTime.Parse("01-03-2021")},
            new Lendings{ReaderID=9,BookID=2,LendingDate=DateTime.Parse("26-01-2021"),ReturnDate=DateTime.Parse("06-02-2021")},
            new Lendings{ReaderID=9,BookID=4,LendingDate=DateTime.Parse("29-01-2021"),ReturnDate=DateTime.Parse("16-02-2021")},
            new Lendings{ReaderID=9,BookID=19,LendingDate=DateTime.Parse("19-03-2021"),ReturnDate=DateTime.Parse("10-04-2021")},
            new Lendings{ReaderID=10,BookID=14,LendingDate=DateTime.Parse("01-02-2021"),ReturnDate=DateTime.Parse("15-02-2021")},
            new Lendings{ReaderID=10,BookID=9,LendingDate=DateTime.Parse("04-03-2021"),ReturnDate=DateTime.Parse("20-03-2021")},
            new Lendings{ReaderID=11,BookID=25,LendingDate=DateTime.Parse("13-03-2021"),ReturnDate=DateTime.Parse("27-03-2021")},
            new Lendings{ReaderID=12,BookID=24,LendingDate=DateTime.Parse("02-01-2021"),ReturnDate=DateTime.Parse("14-01-2021")},
            new Lendings{ReaderID=12,BookID=16,LendingDate=DateTime.Parse("02-01-2021"),ReturnDate=DateTime.Parse("17-01-2021")},
            new Lendings{ReaderID=12,BookID=17,LendingDate=DateTime.Parse("15-03-2021"),ReturnDate=DateTime.Parse("25-03-2021")},
            new Lendings{ReaderID=13,BookID=11,LendingDate=DateTime.Parse("20-03-2021"),ReturnDate=DateTime.Parse("28-03-2021")},
            new Lendings{ReaderID=13,BookID=21,LendingDate=DateTime.Parse("04-02-2021"),ReturnDate=DateTime.Parse("21-02-2021")},
            new Lendings{ReaderID=14,BookID=20,LendingDate=DateTime.Parse("05-02-2021"),ReturnDate=DateTime.Parse("11-02-2021")},
            new Lendings{ReaderID=15,BookID=22,LendingDate=DateTime.Parse("29-01-2021"),ReturnDate=DateTime.Parse("10-02-2021")},
            new Lendings{ReaderID=15,BookID=23,LendingDate=DateTime.Parse("08-01-2021"),ReturnDate=DateTime.Parse("19-01-2021")},
            new Lendings{ReaderID=15,BookID=8,LendingDate=DateTime.Parse("07-03-2021"),ReturnDate=DateTime.Parse("22-03-2021")},
            new Lendings{ReaderID=15,BookID=15,LendingDate=DateTime.Parse("07-03-2021"),ReturnDate=DateTime.Parse("20-03-2021")},
            new Lendings{ReaderID=16,BookID=1,LendingDate=DateTime.Parse("27-02-2021"),ReturnDate=DateTime.Parse("09-03-2021")},
            new Lendings{ReaderID=17,BookID=24,LendingDate=DateTime.Parse("15-01-2021"),ReturnDate=DateTime.Parse("27-01-2021")},
            new Lendings{ReaderID=17,BookID=3,LendingDate=DateTime.Parse("11-03-2021"),ReturnDate=DateTime.Parse("24-04-2021")},
            new Lendings{ReaderID=18,BookID=16,LendingDate=DateTime.Parse("20-04-2021"),ReturnDate=DateTime.Parse("05-05-2021")},
            new Lendings{ReaderID=18,BookID=4,LendingDate=DateTime.Parse("21-03-2021"),ReturnDate=DateTime.Parse("04-04-2021")},
            new Lendings{ReaderID=18,BookID=9,LendingDate=DateTime.Parse("21-03-2021"),ReturnDate=DateTime.Parse("27-03-2021")},
            new Lendings{ReaderID=19,BookID=6,LendingDate=DateTime.Parse("18-02-2021"),ReturnDate=DateTime.Parse("26-02-2021")},
            new Lendings{ReaderID=20,BookID=10,LendingDate=DateTime.Parse("21-02-2021"),ReturnDate=DateTime.Parse("11-03-2021")},
            new Lendings{ReaderID=20,BookID=2,LendingDate=DateTime.Parse("08-03-2021"),ReturnDate=DateTime.Parse("17-03-2021")},
            new Lendings{ReaderID=21,BookID=25,LendingDate=DateTime.Parse("01-05-2021"),ReturnDate=DateTime.Parse("19-05-2021")},
            new Lendings{ReaderID=22,BookID=18,LendingDate=DateTime.Parse("16-04-2021"),ReturnDate=DateTime.Parse("28-04-2021")},
            new Lendings{ReaderID=22,BookID=5,LendingDate=DateTime.Parse("25-02-2021"),ReturnDate=DateTime.Parse("05-03-2021")},
            new Lendings{ReaderID=23,BookID=22,LendingDate=DateTime.Parse("11-03-2021"),ReturnDate=DateTime.Parse("21-03-2021")},
            new Lendings{ReaderID=24,BookID=7,LendingDate=DateTime.Parse("03-02-2021"),ReturnDate=DateTime.Parse("12-03-2021")},
            new Lendings{ReaderID=25,BookID=13,LendingDate=DateTime.Parse("08-02-2021"),ReturnDate=DateTime.Parse("01-03-2021")},
            new Lendings{ReaderID=26,BookID=20,LendingDate=DateTime.Parse("17-03-2021"),ReturnDate=DateTime.Parse("11-04-2021")},
            new Lendings{ReaderID=27,BookID=23,LendingDate=DateTime.Parse("20-02-2021"),ReturnDate=DateTime.Parse("10-04-2021")},
            new Lendings{ReaderID=28,BookID=21,LendingDate=DateTime.Parse("23-02-2021"),ReturnDate=DateTime.Parse("06-03-2021")},
            new Lendings{ReaderID=29,BookID=24,LendingDate=DateTime.Parse("28-03-2021"),ReturnDate=DateTime.Parse("02-04-2021")},
            new Lendings{ReaderID=30,BookID=8,LendingDate=DateTime.Parse("01-04-2021"),ReturnDate=DateTime.Parse("13-05-2021")},
            };
            foreach (Lendings l in lendings)
            {
                context.Lending.Add(l) ;
            }
            context.SaveChanges();
        }
    }
}
