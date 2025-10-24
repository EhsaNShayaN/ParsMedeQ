import {Profile} from './Login';

export interface TicketResponse {
  data: Ticket[];
  totalCount: number;
}

export interface Ticket {
  id: number;
  profileId: string;
  title: string;
  description: string;
  status: number;
  statusText: string;
  mediaId: number;
  code: number;
  answers: TicketAnswers[];
  profile: Profile;
  creationDate: string;
}

export interface TicketAnswers {
  id: number;
  ticketId: string;
  profileId: string;
  answer: string;
  mediaId: number;
  creationDate: string;
}
