import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class AppService
{

  constructor(
    private translate: TranslateService,
    private http: HttpClient) { }

  public async loadTranslations(module: string): Promise<void>
  {
    const path = './assets/translation/' + module + '/' + this.translate.getBrowserLang() + '.json';

    await this.http.get(path).toPromise()
      .then((res) =>
      {
        const lang = this.translate.getBrowserLang();
        this.translate.setTranslation(lang, res, true);
      });
  }
}
