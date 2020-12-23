import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { CookieService } from 'ngx-cookie-service';
import { Cliente } from '../models/Cliente';
import { environment } from 'src/environments/environment';
import { map, tap } from 'rxjs/operators';
import { AccountService } from '../services/account.service';
import {
  ActivatedRoute,
  ActivatedRouteSnapshot,
  Router,
} from '@angular/router';
import { EMPTY } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Interceptor implements HttpInterceptor {
  private token: string = '';
  private refreshToken: string = '';
  private clienteId: string | undefined;

  constructor(
    private cookieService: CookieService,
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  // intercept(
  //   request: HttpRequest<any>,
  //   next: HttpHandler
  // ): Observable<HttpEvent<any>> {
  //   if (this.cookieService.check('emb_user')) {
  //     let obj: Cliente = JSON.parse(this.cookieService.get('emb_user'));
  //     this.token = obj.Token;
  //   }

  //   if (request.url.startsWith(environment.API)) {
  //     request = request.clone({
  //       setHeaders: {
  //         Authorization: 'Bearer ' + this.token,
  //       },
  //     });
  //   }

  //   return next.handle(request);
  // }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.cookieService.check('emb_user')) {
      let obj: Cliente = JSON.parse(this.cookieService.get('emb_user'));
      this.token = obj.Token;
      this.refreshToken = obj.RefreshToken;
      this.clienteId = obj.Id;
    }    

    if (request.url.startsWith(environment.API)) {
      request = request.clone({
        setHeaders: {
          Authorization:
            'Bearer ' +
            (request.url.includes('refreshtoken')
              ? this.refreshToken
              : this.token),
        },
      });
    }    

    // return next.handle(request).pipe(
    //   tap(
    //     () => {},
    //     (err) => {
    //       if (err.status == 401) {
    //         console.log('401', err)
    //         return this.accountService.RefreshToken(this.clienteId).pipe(
    //           tap((val) => {
    //             if (val.Dados) {
    //               this.cookieService.set(
    //                 'emb_user',
    //                 JSON.stringify({
    //                   Id: val.Dados.Id,
    //                   Token: val.Dados.Token,
    //                   RefreshToken: val.Dados.RefreshToken,
    //                 })
    //               );

    //               this.router.navigateByUrl(request.url);
    //             }
    //           },
    //           err => {
    //             console.log('else', err)
    //           })
    //         ).subscribe();
    //       } else {
    //         return new Observable();
    //       }
    //     }
    //   )
    // );

    return next.handle(request).pipe(
      tap(
        () => {},
        (err) => {
          if (err.status != 401) {
            this.router.navigate['login'];
            return EMPTY;
          }

          return this.accountService.RefreshToken(this.clienteId).subscribe(
            (success) => {
              if (success.Dados) {
                this.cookieService.set(
                  'emb_user',
                  JSON.stringify({
                    Id: success.Dados.Id,
                    Token: success.Dados.Token,
                    RefreshToken: success.Dados.RefreshToken,
                  })
                );                

                this.router.navigate([this.router.url]).then(() => {
                  window.location.reload();
                });                
              }
            },
            (err) => {
              this.router.navigate['login'];
            }
          );
        }
      )
    );
  }
}
