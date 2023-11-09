import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { TokenService } from '../services/token.service';
export const permissaoGuard = (activatedRouteSnapshot: ActivatedRouteSnapshot) => {
  const router = inject(Router);
  const tokenService = inject(TokenService);

  return validateGuardToken();

  function validateGuardToken() {
    var role = activatedRouteSnapshot.data['role']
    if(role){
      const user = tokenService.decryptToken();

      if(role.indexOf(user.role) === -1){
        router.navigate(['/billing']);
        return false;
      }
    }

    return true;
  }
}
