import { Component, OnInit } from '@angular/core';
import { AppService } from '../../../core/app.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less']
})
export class DashboardComponent implements OnInit
{
  constructor(private appService: AppService)
  {
  }

  async ngOnInit()
  {
    await this.appService.loadTranslations('dashboard');
  }

}
