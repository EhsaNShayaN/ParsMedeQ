export interface Article {
  id: number;
  title: string;
  author: string;
  publishDate: string;
  summary: string;
  content: string;
  thumbnail: string;
  related?: Article[];
}
