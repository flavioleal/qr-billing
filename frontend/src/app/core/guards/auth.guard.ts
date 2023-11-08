import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
export const authGuard = () => {
  const router = inject(Router);
   const tokenService = inject(TokenService);

  return validateGuardToken();

  function validateGuardToken() {

    const tokenExpired = tokenService.tokenExpired();
    const user = tokenService.decryptToken();
    if (tokenExpired || !user.unique_name) {
       router.navigate(['/login']);
    }

    return !tokenExpired;
  }
}
