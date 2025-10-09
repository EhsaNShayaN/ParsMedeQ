import {AngularEditorConfig} from '@kolkov/angular-editor';

export function getCustomEditorConfigs(): AngularEditorConfig {
  return {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    // placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Alborzchem',
    toolbarHiddenButtons: [
      ['bold']
    ],
    customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    sanitize: false,
    fonts: [
      { class: 'arial', name: 'Arial' },
      { class: 'times-new-roman', name: 'Times New Roman' },
      { class: 'calibri', name: 'Calibri' },
      { class: 'comic-sans-ms', name: 'Comic Sans MS' },

      // 👇 Add your custom font here
      { class: 'Alborzchem', name: 'Alborzchem' },
      { class: 'Alborzchem_en', name: 'Alborzchem_en' },
    ],
  };
}
