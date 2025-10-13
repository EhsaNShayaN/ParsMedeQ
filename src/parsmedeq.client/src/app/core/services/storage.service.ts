import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class StorageService {
  private readonly key = 'anonymousId';

  getAnonymousId(forQueryString: boolean = true): string | null {
    let anonId = localStorage.getItem(this.key);
    if (!anonId) {
      anonId = crypto.randomUUID(); // تولید شناسه یکتا
      localStorage.setItem(this.key, anonId);
    }
    return forQueryString ? `?anonymousId=${anonId}` : anonId;
  }

  clearAnonymousId() {
    localStorage.removeItem(this.key);
  }
}
