import { Component, Injectable, Injector } from '@angular/core';
import 'rxjs/add/operator/do';
import { environment } from '../../environments/environment';
import 'app/static/popup/Popup';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Popup } from '../static/popup/Popup';
import { Loading } from '../static/loading/Loading';

export class BaseService
{
  protected http: HttpClient;
  protected apiUrl = environment.APIURL;

  constructor(private injector: Injector)
  {
    this.http = this.injector.get(HttpClient);
  }

  get<T>(path: string, param: any = '', showError = true, apiUrl = this.apiUrl): Observable<any | T>
  {
    Loading.show();

    const observer = this.http.get<T>(apiUrl + path + '/' + param, {}).pipe(

      catchError(err =>
      {
        if (showError)
        {
          Popup.error(err);
        }
        return of([]);
      }),
      finalize(() => Loading.hide())
    );

    return observer;
  }

  post<T>(path: string, creds: any = '', showError = true, showLoading = true): Observable<any | T>
  {
    Loading.show();

    const observer = this.http.post<T>(this.apiUrl + path, creds, {}).pipe(

      catchError(err =>
      {
        if (showError)
        {
          Popup.error(err);
        }
        return of([]);
      }),
      finalize(() => Loading.hide())
    );

    return observer;
  }


  put<T>(path: string, creds: any = '', showError = true, showLoading = true): Observable<any | T>
  {
    Loading.show();

    const observer = this.http.put<T>(this.apiUrl + path, creds, {}).pipe(

      catchError(err =>
      {
        if (showError)
        {
          Popup.error(err);
        }
        return of([]);
      }),
      finalize(() => Loading.hide())
    );

    return observer;
  }

  delete(path: string, id: any = ''): Observable<any>
  {
    Loading.show();

    const observer = this.http.delete(this.apiUrl + path, {}).pipe(

      catchError(err =>
      {
        Popup.error(err);
        return of([]);
      }),
      finalize(() => Loading.hide())
    );

    return observer;
  }
}
