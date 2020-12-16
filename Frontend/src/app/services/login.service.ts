import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
import { Produto } from '../models/Produto';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }  

  public Login(Email: string, Senha: string) {
    return this.http
      .post<Cliente>(`${environment.API}account/login`, { Email, Senha })
      .pipe(take(1));
  }

}
