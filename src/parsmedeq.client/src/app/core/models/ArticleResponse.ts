/*import {Profile} from './UserResponse';
import {Journal} from './JournalResponse';*/
import {Paginated} from './Pagination';

export interface ArticleResponse extends Paginated {
  data: Article[];
}

export interface Article {
  id: string;
  secondId: string;
  categoryId: string;
  categoryTitle: string;
  title: string;
  abstract: string;
  description: string;
  anchors: ArticleAnchor[];
  image: string;
  mimeType: string;
  keywords: string;
  journalId: string;
  downloadCount: number;
  language: string;
  publishDate: string;
  isVip: boolean;
  pinned: boolean;
  price: number;
  discount: number;
  expirationDate: string;
  expirationTime: string;
  doc: string;
  registered: boolean;
  //authors: Profile[];
  //journal: Journal;
  creationDate: string;
  showInChem: boolean;
  showInAcademy: boolean;
}

export interface ArticleAnchor {
  id: string;
  name: string;
  desc: string;
}
