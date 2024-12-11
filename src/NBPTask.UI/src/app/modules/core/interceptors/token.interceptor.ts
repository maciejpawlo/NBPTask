import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from '../services/token.service';

const NOT_PROTECTED_ROUTES : string[] = [ "auth" ];

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {

  if(NOT_PROTECTED_ROUTES.some(x => req.url.includes(x))){
    return next(req);
  }

  const tokenService = inject(TokenService);
  const token = tokenService.getToken();
  if(!token){
    return next(req);
  }

  req = req.clone({
    setHeaders: {
      "Authorization" : `Bearer ${token}`
    }
  });

  return next(req);
};
