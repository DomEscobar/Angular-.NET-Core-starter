import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor
{

  constructor()
  {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
  {
    const token = localStorage.getItem('token');
    if (token != null)
    {
      return next.handle(req.clone({
        setHeaders: {
          'Authentication': 'Bearer ' + token,
          'content-type': 'application/json'
        }
      }));
    }
    else
    {
      return next.handle(req);
    }
  }
}
