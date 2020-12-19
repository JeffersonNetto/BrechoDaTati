import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { CookieService } from 'ngx-cookie-service';
import { Cliente } from '../models/Cliente';

@Injectable({
  providedIn: 'root',
})
export class Interceptor implements HttpInterceptor {
  private token: string | undefined;

  constructor(private cookieService: CookieService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.cookieService.check('emb_user')) {
      let obj: Cliente = JSON.parse(this.cookieService.get('emb_user'));
      this.token = obj.Token;
    }

    request = request.clone({
      setHeaders: {
        Authorization: 'Bearer ' + this.token,
      },
    });

    return next.handle(request);
  }
}
