// src/app/models/product.model.ts
export interface ProductSpecification {
  key: string;
  value: string;
}

export interface Product {
  id: number;
  title: string;
  description: string;
  price: number;              // قیمت پایه (عدد کامل، تومان)
  discountPercent?: number;   // درصد تخفیف (مثلاً 10 برای 10%)
  category: string;
  coverImage: string;
  gallery: string[];
  specifications: ProductSpecification[];
  inStock?: boolean;
  brand?: string;
  rating?: number; // 0..5
}
