import { Injectable } from '@angular/core';
import { jwtDecode, JwtPayload } from "jwt-decode";

const TOKEN_KEY = 'access-token';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  saveToken(token: string) : void {
    window.localStorage.removeItem(TOKEN_KEY);
    window.localStorage.setItem(TOKEN_KEY, token);
  }

  removeToken() : void {
    window.localStorage.clear();
  }

  isTokenExpired() : boolean {
    const token = window.localStorage.getItem(TOKEN_KEY);
    if(!token){
      return false;
    }
    const decodedToken: JwtPayload = jwtDecode(token);
    const expirationDate = new Date(decodedToken.exp! * 1000);
    const now = new Date()
    return expirationDate < now;
  }

  getToken(): string | null {
    return window.localStorage.getItem(TOKEN_KEY);
  }
}
