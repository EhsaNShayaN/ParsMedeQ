# Translation System Setup

This project has been configured with internationalization (i18n) support using `@ngx-translate/core` with Persian (fa) as the default language and English (en) as an alternative.

## Configuration

### Default Language
- **Default Language**: Persian (fa)
- **Supported Languages**: Persian (fa), English (en)

### Translation Files
Translation files are located in `src/assets/i18n/`:
- `fa.json` - Persian translations
- `en.json` - English translations

### Current Test Translations
```json
{
  "COMMON": {
    "WELCOME": "خوش آمدید",
    "TEST_MESSAGE": "پیام تست برای ترجمه"
  }
}
```

## Usage

### In Templates
Use the translate pipe to display translated text:
```html
<h2>{{ 'COMMON.WELCOME' | translate }}</h2>
<p>{{ 'COMMON.TEST_MESSAGE' | translate }}</p>
```

### In Components
Inject the TranslateService to use translations programmatically:
```typescript
import { TranslateService } from '@ngx-translate/core';

constructor(private translateService: TranslateService) {}

// Get translated text
this.translateService.get('COMMON.WELCOME').subscribe((res: string) => {
  console.log(res);
});
```

### Language Switching
Use the LanguageService to change languages:
```typescript
import { LanguageService } from './core/services/language.service';

constructor(private languageService: LanguageService) {}

changeLanguage(lang: string) {
  this.languageService.setLang(lang);
}
```

## Testing

1. Navigate to the home page (`/`)
2. You should see the welcome message in Persian by default
3. Click the language buttons to switch between Persian and English
4. The text should change accordingly

## Adding New Translations

1. Add new keys to both `fa.json` and `en.json` files
2. Use the translate pipe in your templates: `{{ 'YOUR.KEY' | translate }}`
3. The translation will automatically update when the language is changed

## Language Persistence

The selected language is stored in localStorage and will persist across browser sessions. 