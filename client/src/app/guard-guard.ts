import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn } from '@angular/router';
import { AuthService } from './core/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
 const authService = inject(AuthService);
 return authService.isAuthenticated();
};
