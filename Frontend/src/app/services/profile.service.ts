import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private http: HttpClient,
    private cookieService: CookieService    
  ) { }

  // private GetHeaderWithToken() : HttpHeaders {

  //   let obj = JSON.parse(this.cookieService.get('emb_user'))    
    
  //   return new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': `Bearer ${ obj?.Token }` })
  // }

  public GetById(Id: string) {
    //return this.http.get<Cliente>(`${environment.API}cliente/${Id}`, { headers: this.GetHeaderWithToken() }).pipe(first());
    return this.http.get<Cliente>(`${environment.API}cliente/${Id}`).pipe(first());        
  }
}
