import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'mainpage',
        loadComponent: () =>
        import('./mainpage/mainpage.component').then(m => m.MainpageComponent)
    },
];
