import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }  

  public Register(cliente: Cliente) {
    return this.http
      .post<Retorno<Cliente>>(`${environment.API}account/register`, cliente)
      .pipe(first());
  }
}
