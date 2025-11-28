export enum Tables {
  Article = 1,
  News = 2,
  Clip = 3,
  Product = 4,
  Payment = 5,
  Order = 6,
  Ticket = 7,
  TicketAnswers = 8,
}

export enum OrderStatus {
  Pending = 0,
  Paid = 1,
  Shipped = 2,
  Completed = 3,
  Cancelled = 4,
  Error = 5,
}

export enum PaymentMethods {
  Gateway = 0,
}

export enum PaymentStatus {
  Pending = 0,
  Success = 1,
  Failed = 2,
  Refunded = 3,
}

export enum PaymentLogTypes {
  Request = 0,
  Response = 1,
  Callback = 2,
}

export enum SectionType {
  mainImage = 1,
  centers = 2,
  services = 3,
  advantages = 4,
  about = 5,
  contact = 6,
  bottomImage = 7,
  ForStudy = 8,
}

export const MainSections: any[] = [
  {id: 1, sectionId: 1, type: SectionType.mainImage, title: 'SECTIONS.MAIN_IMAGE'},
  {id: 2, sectionId: 2, type: SectionType.centers, title: 'SECTIONS.CENTERS'},
  {id: 3, sectionId: 3, type: SectionType.services, title: 'SECTIONS.SERVICES'},
  {id: 4, sectionId: 4, type: SectionType.advantages, title: 'SECTIONS.ADVANTAGES'},
  {id: 5, sectionId: 5, type: SectionType.about, title: 'SECTIONS.ABOUT'},
  {id: 6, sectionId: 6, type: SectionType.contact, title: 'SECTIONS.CONTACT'},
  {id: 7, sectionId: 7, type: SectionType.bottomImage, title: 'SECTIONS.BOTTOM_IMAGE'},
  {id: 8, sectionId: 8, type: SectionType.ForStudy, title: 'SECTIONS.FOR_STUDY'},
];
