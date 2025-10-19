export interface Clip {
  id: number;
  title: string;
  description: string;
  thumbnail: string;
  videoUrl: string;
  category: string;
  publishDate: string; // ISO
  tags?: string[];
  related?: Clip[];
}
