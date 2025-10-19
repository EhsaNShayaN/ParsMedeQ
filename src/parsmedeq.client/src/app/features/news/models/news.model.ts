export interface News {
  id: number;
  title: string;
  summary: string;
  content: string;
  coverImage: string;
  category: string;
  publishDate: string; // ISO date string
  related?: News[];
}
