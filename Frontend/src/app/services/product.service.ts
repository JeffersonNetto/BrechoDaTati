import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Produto } from '../models/Produto';
import { BehaviorSubject, EMPTY } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {  

  produtos: BehaviorSubject<Produto[]> = new BehaviorSubject(Array.of())

  constructor(private http: HttpClient) { }

  public Get() {
    return this.http.get<Produto[]>(`${environment.API}produto`).pipe(take(1));
  }

  public GetById(Id: string) {
    return this.http
      .get<Produto>(`${environment.API}produto/${Id}`)
      .pipe(take(1));
  }

  public GetBySlug(Slug: string) {
    return this.http
      .get<Produto>(`${environment.API}produto/slug/${Slug}`)
      .pipe(take(1));
  }

  public VerificarEstoque(Id: string) {
    return this.http
      .get<number>(`${environment.API}produto/${Id}/estoque`)
      .pipe(take(1));
  }

  public IncrementarEstoque(Id: string) {
    return this.http
      .get<Produto[]>(`${environment.API}produto/${Id}/incrementarestoque`)
      .pipe(take(1));
  }

  public DecrementarEstoque(Id: string) {
    return this.http
      .get<Produto[]>(`${environment.API}produto/${Id}/decrementarestoque`)
      .pipe(take(1));
  }
  
}
