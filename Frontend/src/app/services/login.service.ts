import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  isUserLogged: BehaviorSubject<boolean>;

  constructor(
    private http: HttpClient    
  ) { 

    this.isUserLogged = new BehaviorSubject(false);
  }

  public Login(Email: string, Senha: string) {
    return this.http.post<Retorno<Cliente>>(`${environment.API}account/login`, { Email, Senha }).pipe(first());        
  }  
}
