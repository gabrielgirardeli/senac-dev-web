import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { Categorias } from './pages/categorias/categorias';

export const routes: Routes = [
    {
        path:'',
        component: Dashboard
    },
    {
        path:'config/categorias',
        component: Categorias
    }
];
