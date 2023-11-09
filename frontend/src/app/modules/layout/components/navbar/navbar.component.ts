import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/core/services/user.service';
import { IUserOutput } from 'src/app/modules/account/interfaces/user-output.interface';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  user$: Observable<IUserOutput>;
  constructor(private userService: UserService) {
    this.user$ = this.userService.$user;
  }

  logout(){
    this.userService.logout();
  }
}
