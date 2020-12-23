import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private http: HttpClient    
  ) { }

  public Login(Email: string, Senha: string) {
    return this.http.post<Retorno<Cliente>>(`${environment.API}account/login`, { Email, Senha }).pipe(first());        
  } 

  public Register(cliente: Cliente) {
    return this.http
      .post<Retorno<Cliente>>(`${environment.API}account/register`, cliente)
      .pipe(first());
  }

  public RefreshToken(clienteId: string) {
    return this.http.get<Retorno<Cliente>>(`${environment.API}account/refreshtoken/${clienteId}`).pipe(first());
  }
}
