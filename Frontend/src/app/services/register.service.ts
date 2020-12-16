import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }  

  public Register(cliente: Cliente) {
    return this.http
      .post<Cliente>(`${environment.API}account/register`, cliente)
      .pipe(first());
  }
}
