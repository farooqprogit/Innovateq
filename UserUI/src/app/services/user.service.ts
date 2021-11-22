import { Injectable } from "@angular/core"
import { Observable, BehaviorSubject, throwError  } from "rxjs"
import { HttpClient, HttpHeaders,HttpParams,HttpResponse   } from "@angular/common/http"
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private API_URL : any= environment.API_URL;
  constructor(private http: HttpClient) { }

  getBlogs(): Observable<any> {
      return this.http.get<any>(this.API_URL + "posts")
      .pipe(
          tap(status => console.log("status: " + status)),
          catchError(this.handleError)
      );
  }
  getUsers(): Observable<any> {
    return this.http.get<any>(this.API_URL + "users")
    .pipe(
        tap(status => console.log("status: " + status)),
        catchError(this.handleError)
    );
}
  private handleError(error: any) {
    console.error(error);
    return throwError(error);
}
}
