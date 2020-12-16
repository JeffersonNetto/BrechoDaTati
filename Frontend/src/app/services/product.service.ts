import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Produto } from '../models/Produto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public Get() {    
    return this.http.get<Produto[]>(`${environment.API}produto`).pipe(take(1))
  }

  public GetById(Id: string) {    
    return this.http.get<Produto>(`${environment.API}produto/${Id}`).pipe(take(1))
  }

}
