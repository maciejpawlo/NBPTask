import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

export const unauthGuard: CanActivateFn = () => {
  const router: Router = inject(Router);
  const isLoggedIn: boolean = inject(AuthenticationService).isLoggedIn();
  return !isLoggedIn || router.parseUrl('');
};
