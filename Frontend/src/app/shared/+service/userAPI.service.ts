import { Injectable, Injector } from '@angular/core';
import { BaseService } from '../../core/baseService';
import { Observable } from 'rxjs';
import { User } from '../+model/user';

@Injectable()
export class UserAPIService extends BaseService
{
  readonly APIURL = 'User';

  constructor(injector: Injector)
  {
    super(injector);
  }

  getUser(userId: number): Observable<User>
  {
    return this.get<User>(`${ this.APIURL }/${ userId }`);
  }
}
