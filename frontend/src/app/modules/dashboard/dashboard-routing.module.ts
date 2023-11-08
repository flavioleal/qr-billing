import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BillingFormComponent } from './components/billing-form/billing-form.component';
import { BillingListComponent } from './components/billing-list/billing-list.component';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: '', redirectTo: 'billing', pathMatch: 'full'
  },
  {
    path: 'billing', component: DashboardComponent,
    children: [
      {
        path: '',
        component: BillingListComponent
      },
      {
        path: 'add',
        component: BillingFormComponent
      },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
