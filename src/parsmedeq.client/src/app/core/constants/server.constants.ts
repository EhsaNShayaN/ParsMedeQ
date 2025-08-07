export class ServerConstants {
}

export enum Tables {
  Article = 1,
  Journal = 2,
  Advisor = 3,
  ChiefEditor = 4,
  Book = 5,
  News = 6,
  AlborzPublication = 7,
  Notice = 8,
  Webinar = 9,
  Course = 10,
  Podcast = 11,
  Media = 12,
  Product = 13,
  CourseMovie = 14,
  Standard = 15,
  UserCourseMovieMedia = 16,
  Order = 17,
  Thesis = 18,
  ProductVariant = 19,
  Contract = 20,
  Ticket = 21,
  TicketAnswers = 22,
  JournalScientific = 23,
  Payment = 24,
  UserScore = 25,
  Clip = 26,
  Marketer = 27,
}

export function getWebinarTypes() {
  return [
    {id: 1, title: 'حضوری'},
    {id: 2, title: 'آنلاین'},
    {id: 3, title: 'حضوری-آنلاین'},
  ];
}

