import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { first, map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
import { Produto } from '../models/Produto';
import { Retorno } from '../models/Retorno';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) { }

  public Login(Email: string, Senha: string) {
    let result = this.http.post<Retorno<Cliente>>(`${environment.API}account/login`, { Email, Senha }).pipe(first());
    
    console.log('antes do cookie')

    result.pipe(map(_ => {
      console.log('cookie')
      this.cookieService.set(`${_.Dados?.Id}`, JSON.stringify(_.Dados))
    }));
    
    return result;
  }
}
