import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  constructor(
    private http: HttpClient    
  ) { }

  public GetFromCache<T>(key: string) {    
    return this.http.get<T>(`${environment.API}account/cache/${key}`).pipe(first());
  }  
}
