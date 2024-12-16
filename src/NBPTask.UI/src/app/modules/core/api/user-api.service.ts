import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SignIn } from './requests/sign-in';
import { AuthenticationDto } from './responses/authentication-dto';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  private http: HttpClient = inject(HttpClient);
  private apiUrl: string = `${environment.apiUrl}/user`

  signIn(request: SignIn): Observable<AuthenticationDto> {
    return this.http.post<AuthenticationDto>(`${this.apiUrl}/sign-in`, request);
  }
}
