import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthenticationGuard implements CanActivate
{
  constructor(
    private router: Router) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean>
  {
    return true;
  }

  private showInit(state: RouterStateSnapshot): void
  {
    this.router.navigate(['/init'], { queryParams: { redirect: state.url }, replaceUrl: true });
  }
}
