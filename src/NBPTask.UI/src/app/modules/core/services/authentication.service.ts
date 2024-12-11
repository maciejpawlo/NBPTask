import { inject, Injectable } from '@angular/core';
import { UserApiService } from '../api/user-api.service';
import { SignIn } from '../api/requests/sign-in';
import { AuthenticationDto } from '../api/responses/authentication-dto';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private userApi: UserApiService = inject(UserApiService);
  private tokenService: TokenService = inject(TokenService);

  signIn(username: string, password: string): Observable<void> {
    const request: SignIn = {
      username: username,
      password: password
    };
    return this.userApi.signIn(request)
      .pipe(
        tap((response: AuthenticationDto) => {
          this.tokenService.saveToken(response.token);
        }),
        map(() => {
          return void 0;
        }));
  }

  logout(callback: () => void): void {
    this.tokenService.removeToken();
    callback();
  }

  isLoggedIn(): boolean {
    return !this.tokenService.isTokenExpired();
  }
}
