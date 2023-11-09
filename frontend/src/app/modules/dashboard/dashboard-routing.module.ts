import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { permissaoGuard } from 'src/app/core/guards/permission.guard';

import { BillingFormComponent } from './components/billing-form/billing-form.component';
import { DashboardComponent } from './dashboard.component';
import { AdminComponent } from './pages/admin/admin.component';
import { MerchantComponent } from './pages/merchant/merchant.component';

const routes: Routes = [
  {
    path: '', redirectTo: 'lojista', pathMatch: 'full'
  },
  {
    path: '', component: DashboardComponent,
    children: [
      {
        path: '', component: MerchantComponent,
        canActivate: [permissaoGuard],
        data: {
          role: 'lojista'
        },
      },
      {
        path: 'nova-cobranca',
        component: BillingFormComponent,
        canActivate: [permissaoGuard],
        data: {
          role: 'lojista'
        }
      },
      {
        path: 'admin', component: AdminComponent,
        canActivate: [permissaoGuard],
        data: {
          role: 'admin'
        }
      },

    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
